# How to make a federal exchange between 2 RabbitMQs

1. Make sure you have a version of RabbitMQ that has Federation Plugin
- if you do not have one, then:  
`docker run --restart=always -d --hostname node1 -e RABBITMQ_NODENAME=UNIQUE-NAME-rabbitmq --name rabbitmq -p 15672:15672 -p 5672:5672 elders/rabbitmq:3.8.3` 

    *Make sure you pick a unique name in the RABBITMQ_NODENAME environment variable*

3. In the downstream RabbitMQ go to Admin Tab-> Federation Upstream and add a new upstream with the following settings:
- **Virtual Host** - Here you pick the downstream public VHost where you want to receive the messages
- **Uri** - Put the Uri to the Upstream RMQ VHost and the credentials for the RMQ user amqp://guest:guest@rmq.integration.onebigsplash.com:5672/unicom-public
- **Name** - Name of the federation upstream
- **Exchange** - Upstream Exchange that you want to bind to

**The federation exchange that we just added will bind to the upstream federated exchange.**

4. Go to *Admin --> Policies* and add a policy. That policy is the connection between the federated exchange we created in step 3 and the exchanges in the downstream RabbitMQ, where we want to receive the messages from the Upstream
- **Virtual host** - We put the public virtual host on the downstream RabbitMQ
- **Name** - Whatever
- **Pattern** - Here you put RegEx pattern for the exchanges we want to receive
- **Apply to** - Exchanges
5. Go to *Admin-> Federation Upstreams again* and edit the Max Hops for our downstream virtual host 
- **Max Hops** - that means that the messages we want to receive must have maximum 2 hops. A hop increases when a message goes through exchange. It's useful to avoid circular reference for example.
- **How to Update the Hops** - click add upstream and fill the same information for the one u want to update

** This tutorial is also viable if you want to receive signals from another queue (e.g. receiving signals from Newton INT locally in your VAPT RabbitMQ). The one thing that you should be careful with is the naming of the VHosts because it can cause mismatches if it's not right.

## Example

1. I have an old version of RabbitMq so open docker and type  
`docker rm 54scq1eerdfsa2 -f`  
- `54scq1eerdfsa2` is the hash for my rabbit mq old docker container
- `-f` flag is used to forcefully stop the container if it is currently used
2. Open docker and type:

    `docker run --restart=always -d --hostname node1 -e RABBITMQ_NODENAME=Elder-13-rabbitmq --name rabbitmq -p 15672:15672 -p 5672:5672 elders/rabbitmq:3.8.3` 

3. In my local RabbitMQ  `http://docker-local.com:15672/` i login with guest/guest user and go to `Admin->Federation Upstrreams` and put the following settings:
- **Virtual Host** - unicom-public (VHost of the upstream RMQ)
- **Name** - INT (we name our fed exchange INT, because it will bind to int environment)
- **Uri** - amqp://guest:guest@rmq.integration.onebigsplash.com:5672/unicom-public (the uri of the upstream VHost we want to bind to)
- **Exchange** - PublicEvents (That's the name of the upstream exchange we want to bind to)

    *Now that we have the federated exchanges we achieved the following: The messages that go in the `PublicEvents` exchange in the upstream rabbitmq will now come to our federated exchange `INT`*

4. Go to `Admin --> Policies` and add a policy with the following settings:
- **Virtual host** - unicom-public (That is the VHost of our downstream RabbitMq where we want to receive the messages)
- **Name** - INT (That is the name of the policy and since we receive message from the integration we name it `INT`)
- **Pattern** - PublicEvents$ (Here we add a pattern and the result of the RegEx will be the exchanges that will be bind to our Federated Exchange we just created -> `Int`)
- **Apply to** - Exchanges (Here we say that only the exchanges will bind to the federated upstream)
- **Put in Definition** - federation-upstream	= INT (here we put federation-upstream = {name of our Federated Upstream})
5. Go to `Admin-> Federation Upstreams` again and edit the Max Hops for our `unicom-public-host` VHost to `2`
