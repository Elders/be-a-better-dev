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
+ `sudo openssl passwd -1 > cloud-config-file` will create a cloud config file with your password hash
+ `vi cloud-config-file` add minimum information in the cloud-config file
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
	
- Set up Cassandra with Docker
`docker run --restart=always -d --name docker-cass -p 9042:9042 -p 9160:9160 -p 7199:7199 -p 7001:7001 -p 7000:7000 cassandra`

- Set up RabbitMq with Docker. Make sure that you change "UNIQUENAME" to something unique
`docker run --restart=always -d -e RABBITMQ_NODENAME=docker-UNIQUENAME-rabbitmq --name docker-rabbitmq -p 15672:15672 -p 5672:5672 rabbitmq:3.5.2-management`
	
- Set up ElasticSearch with Docker 
`docker run --restart=always -d --name docker-elastic -p 9200:9200 -p 9300:9300 elasticsearch`
	 