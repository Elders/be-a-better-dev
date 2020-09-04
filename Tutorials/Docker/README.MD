# DOCKER FOR DUMMIES

# Usable containers

- Setup Cassandra with Docker (**NB**: Container memory is limited to 2GB in example):  
`docker run --restart=always -d -m 2g --name cassandra -p 9042:9042 -p 9160:9160 -p 7199:7199 -p 7001:7001 -p 7000:7000 cassandra`

- Setup RabbitMq with Docker. Make sure that you change **UNIQUENAME** to something unique  (*NB*: Container memory is limited to 512MB in example):  
`docker run --restart=always -d -m 512m --hostname node1 -e RABBITMQ_NODENAME=docker-UNIQUENAME-rabbitmq --name rabbitmq -p 15672:15672 -p 5672:5672 elders/rabbitmq:3.8.3`

- Setup ElasticSearch with Docker (**NB**: Container memory is limited to 3GB in example):  
	- Create [docker volume](https://docs.docker.com/engine/reference/commandline/volume_create/)  `docker volume create --name esdata`
	- Create container  
  `docker run --restart=always -d -m 3g --name elasticsearch -p 9200:9200 -p 9300:9300 -e "http.host=0.0.0.0" -e "transport.host=127.0.0.1" -e "discovery.type=single-node" -e "discovery.zen.minimum_master_nodes=1" -e "cluster.name=docker-local" -e "bootstrap.memory_lock=true" -e ES_JAVA_OPTS="-Xms2g -Xmx2g" -v esdata:/usr/share/elasticsearch/data docker.elastic.co/elasticsearch/elasticsearch-oss:7.5.2`
  - `ES_JAVA_OPTS="-Xms2g -Xmx2g"` allows ES to use 2 GB of RAM. Check the machine's available RAM before executing the command above. The docker container is allowed 3 GB as it needs to be more than the 2GB for elastic.
  - For reference [https://www.elastic.co/guide/en/elasticsearch/reference/current/docker.html](https://www.elastic.co/guide/en/elasticsearch/reference/current/docker.html)
	
- Setup Kibana - `docker run --restart=always -d --name kibana --link {elasticsearchcontainer}:elasticsearch -p 5601:5601 kibana:5.2`


- Atlas - `docker run --restart=always --name=atlas -d -p 7101:7101 eldersoss/atlas`

- Setup Cronus.UI - `docker run --restart=always --name=cronus -d -p 8080:8080 elders/cronus:0.2.0`

- Setup Neo4j with Docker (**NB**: Container memory is limited to 4GB in example):  
`docker run --restart=always -d -m 4g --name neo4j --publish=7474:7474 --publish=7687:7687 --cap-add=SYS_RESOURCE -v /var/lib/neo4j/data --env NEO4J_AUTH=none --env NEO4J_dbms_memory_heap_initialSize=3G --env NEO4J_dbms_memory_heap_maxSize=3G --env NEO4J_dbms_memory_pagecache_size=2G neo4j:4.0.0`

- Setup Redis with Docker
  - Standalone
  `docker run --restart=always --name standalone-node -p 6379:6379 -d redis`
  
- Consul for local dev env with docker for windows
`docker run --restart=always -d -p 8500:8500 -p 8400:8400 -p 8300:8300/tcp -p 8301:8301 -p 8302:8302 -p 8300:8300/udp -p 8301:8301/udp -p 8302:8302/udp -p 8600:8600 -p 8600:8600/udp -e ZEUS_OPTIONS="machine+-f" --net=host --name consul eldersoss/consul-zeus:latest agent -node=consul-node -ui -advertise=10.0.75.2 -client=10.0.75.2 -bootstrap -server=true`

- Consul for mynkow  
`docker run --restart=always -d -p 8500:8500 --name consul consul:1.6.2 agent -server -node=mynkow -ui -advertise=127.0.0.1 -bootstrap -client=0.0.0.0`

# Login in a container

`docker exec -it {container-name} bash`

# Stop all running containers

`docker stop $(docker ps -a -q)`

# Docker Remote API

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
ListenStream=4243
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

# Install docker with VirtualBox

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
