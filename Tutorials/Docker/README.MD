#DOCKER FOR DUMMIES#

1. Download and install VirtualBox
	
2. Download and install Linux virtual machine - CoreOs
    - download source: https://coreos.com/os/docs/latest/booting-with-iso.html
    - make sure that virtualization is enabled from the BIOS
    - make sure that hyper v windows feature is turned off
    - run VirtualBox and create new Linux virtual machine
        + Ubuntu 64bit with at least 1 GB of ram and 15 GB of disk space (fixed size)
    - run settings on newly created virtual machine. In network section change "Attached to" to Bridged Adapter
    - run settings on newly created virtual machine. In storage section click add CD/DVD Device and point to downloaded CoreOS image
    - run the VM instance and install the operation system (OS)
        + at this point you have the CoreOS running in "Live CD" mode which means that whatever you do it will be cleaned up when you restart the VM. Steps bellow fixes this.
        + `sudo openssl passwd -1 > cloud-config-file.yml` will create a cloud config file with your password hash
        + `vi cloud-config-file.yml` add minimum information in the cloud-config file
```
#cloud-config
users:
- name: username
  passwd: passwordhash
  groups:
  - sudo
  - docker
```
- `sudo coreos-install -d /dev/sda -C stable -c cloud-config-file.yml` to do the actuall install of CoreOS
- `sudo shutdown -h now` will shutdown the VM
- unmount the CoreOS image

Usable containers
-----------------
- Setup Cassandra with Docker  
`docker run --restart=always -d --name cassandra -p 9042:9042 -p 9160:9160 -p 7199:7199 -p 7001:7001 -p 7000:7000 cassandra`

- Setup RabbitMq with Docker. Make sure that you change **UNIQUENAME** to something unique  
`docker run --restart=always -d -e RABBITMQ_NODENAME=docker-UNIQUENAME-rabbitmq --name rabbitmq -p 15672:15672 -p 5672:5672 rabbitmq:3.6.1-management`
  - To install RabbitMQ plugin to delay messages: [rabbitmq_delayed_message_exchange](http://www.rabbitmq.com/community-plugins.html)
    - `docker exec -it rabbitmq bash` will login to the container instance
    - `apt-get update` will refresh the packages. This is a preparation for installing `curl`
    - `apt-get install curl -y` to install `curl`
    - `curl http://www.rabbitmq.com/community-plugins/v3.6.x/rabbitmq_delayed_message_exchange-0.0.1.ez --output usr/lib/rabbitmq/lib/rabbitmq_server-3.6.1/plugins/rabbitmq_delayed_message_exchange-0.0.1.ez` will download the plugin in the [RabbitMQ plugins folder](https://www.rabbitmq.com/installing-plugins.html)
    - `rabbitmq-plugins enable rabbitmq_delayed_message_exchange` will activate the plugin
  - To reset RabbitMQ instance - this will reset all settings to factory defaults, even users
    - login to the RabbitMQ container
    - `rabbitmqctl stop_app`
    - `rabbitmqctl reset`
    - `rabbitmqctl start_app`
- Setup ElasticSearch with Docker  
`docker run --restart=always -d --name elasticsearch -p 9200:9200 -p 9300:9300 elasticsearch -Des.node.name="docker-local"`
- Setup LOG
  - Setup ElasticSearch for logs with Docker  
  `docker run --restart=always -d --name log -p 9200:9200 -p 9300:9300 elasticsearch -Des.node.name="docker-local"`
  - Setup Kibana  
  *docker run --restart=always -d --name kibana --link {elasticsearchcontainer}:elasticsearch -p 5601:5601 kibana*
  `docker run --restart=always -d --name kibana --link log:elasticsearch -p 5601:5601 kibana`

Login in a container
--------------------
`docker exec -it {container-name} bash`
