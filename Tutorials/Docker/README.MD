DOCKER FOR DUMMIES

1. Download and install VirtualBox (VB)
	
2. Download and install Linux virtual machine (VM) - Ubuntu (for more expirienced linux users we suggest using CoreOs)
	- download source: http://www.ubuntu.com/
	- make sure that virtualization is enabled from the BIOS
	- make sure that hyper v windows feature is turned off
	- run VirtualBox and create new Linux virtual machine
		+ Ubuntu 64bit with at least 1 GB of ram and 15 GB of disk space (fixed size)
	- run settings on newly created virtual machine. In network section change "Attached to" to Bridged Adapter
	- run settings on newly created virtual machine. In storage section click add CD/DVD Device and point to downloaded Linux image
	- run the VM instance and install the operation system (OS)
	
2.1 After the virtual OS is setted up
	- install openssh-server by running the following command - "sudo apt-get install openssh-server"
	- optional: connect with ssh to the machine
	
3. Set up docker
	- install docker - "sudo apt-get install docker.io"
	- enabled docker on boot  - "sudo systemctl enable docker"
	- start docker as a service - "service docker start"

Usable containers
	
- Set up Cassandra with Docker
	- run Cassandra container and map the ports - "docker run --restart=always -d --name docker-cass -p 9042:9042 -p 9160:9160 -p 7199:7199 -p 7001:7001 -p 7000:7000 cassandra"

- Set up RabbitMq with Docker
	- make sure that you change "UNIQUENAME" to something unique
	- run RabbitMq container and map the ports  - "docker run --restart=always -d -e RABBITMQ_NODENAME=docker-UNIQUENAME-rabbitmq --name docker-rabbitmq -p 15672:15672 -p 5672:5672 rabbitmq:3.5.2-management"
	
- Set up ElasticSearch with Docker 
	- run elastic search and map the ports - "docker run --restart=always -d --name docker-elastic -p 9200:9200 -p 9300:9300 elasticsearch"
	 