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
`docker run --restart=always -d -e RABBITMQ_NODENAME=docker-UNIQUENAME-rabbitmq --name rabbitmq -p 15672:15672 -p 5672:5672 elders/rabbitmq:release-1.0.0`
- Setup ElasticSearch with Docker  
`docker run --restart=always -d --name elasticsearch -p 9200:9200 -p 9300:9300 elasticsearch -Des.node.name="docker-local"`
- Setup LOG
  - Setup ElasticSearch for logs with Docker  
  `docker run --restart=always -d --name log -p 9200:9200 -p 9300:9300 elasticsearch -Des.node.name="docker-local"`
  - Setup Kibana  
  *docker run --restart=always -d --name kibana --link {elasticsearchcontainer}:elasticsearch -p 5601:5601 kibana*
  `docker run --restart=always -d --name kibana --link log:elasticsearch -p 5601:5601 kibana`
- Atlas
`docker run --restart=always --name=atlas -d -p 7101:7101 eldersoss/atlas`

- Setup Neo4j with Docker
`docker run --restart=always -d --name neo4j --publish=7474:7474 --publish=7687:7687 --cap-add=SYS_RESOURCE -v /var/lib/neo4j/data neo4j`

Login in a container
--------------------
`docker exec -it {container-name} bash`

Docker Remote API
-----------------
- Setup Amazon based docker
In RHEL7, instead of modifying your docker.service unit file, you can also just edit your /etc/sysconfig/docker file:

```
# /etc/sysconfig/docker

# Modify these options if you want to change the way the docker daemon runs
OPTIONS='--selinux-enabled -H unix:///var/run/docker.sock -H tcp://0.0.0.0:4243'
```

- How to enable docker remote api on CoreOS => https://coreos.com/os/docs/latest/customizing-docker.html
Create a file called /etc/systemd/system/docker-tcp.socket to make Docker available on a TCP socket on port 4243

```
[Unit]
Description=Docker Socket for the API

[Socket]
ListenStream=2375
BindIPv6Only=both
Service=docker.service

[Install]
WantedBy=sockets.target
```

Then enable this new socket:

```
systemctl enable docker-tcp.socket
systemctl stop docker
systemctl start docker-tcp.socket
systemctl start docker
```
