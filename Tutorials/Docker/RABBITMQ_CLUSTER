#DOCKER - SET UP RABBITMQ CLUSTER#

Add the Very First RabbitMQ Node
--------------------------------

1. Create RabbitMQ Container
`docker run --restart=always -d -e RABBITMQ_NODENAME={NODE_NAME} -h {CONTAINER_HOST_NAME} --name {CONTAINER_NAME} -p 15672:15672 -p 5672:5672 eldersoss/rabbitmq`
	
2. Get node's **'.erlang.cookie'** for seed cookie for the cluster **(all nodes cookies should be the same)** and copy it somewhere to use later
`docker exec {CONTAINER_NAME} cat var/lib/rabbitmq/.erlang.cookie`
-----

Add Other Node to the Cluster (repeate for every other node)
------------------------------------------------------------

1. Create RabbitMQ Container to join the cluster 
`docker run --restart=always -d -e RABBITMQ_NODENAME={NODE_NAME} -h {CONTAINER_HOST_NAME} --name {CONTAINER_NAME} --link` `{CONTAINER_MASTER_NAME}:{CONTAINER_MASTER_NAME} -p 15673:15672 -p 5673:5672 eldersoss/rabbitmq`
> ==EVERY TIME YOU ADD NEW NODE, YOU SHOULD LINK THE PREVIOUS CONTAINERS ALSO ( --link {CONTAINER_NAME}:{CONTAINER_NAME} )== 
2. Add the seed **'.erlang.cookie'** to the container 
`docker exec {CONTAINER_NAME} rabbitmqctl stop_app`
`docker exec {CONTAINER_NAME} sh -c "echo '{MATER erlang.cookie}' > /var/lib/rabbitmq/.erlang.cookie"`
`docker restart {CONTAINER_NAME}`

3. Join the node to the cluster
`docker exec {CONTAINER_NAME} rabbitmqctl stop_app`
`docker exec {CONTAINER_NAME} rabbitmqctl join_cluster {PREVIOUS_CONTAINER_NODE_NAME}@{PREVIOUS_CONTAINER_NAME}`
`docker exec {CONTAINER_NAME} rabbitmqctl start_app`
---


If you want High Availability Policy for your Queues
--------------------------------
`docker exec {CLUSTERED_CONTAINER_NAME} rabbitmqctl set_policy ha-policy "^ha\." '{"ha-mode":"all"}' --apply-to queues`

-------


NOTES
---------------------------------------------
- If you try to start a container, which is linked to a stoped container, you will fail. First you should start the linked container, then start the current one
- In **HA-Policy**, after the Master Node goes down, the next Master Node elected is the eldest one
