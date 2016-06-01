#Elastic search, kibana and curator with recurring job setup#

1. Create (working) directory where docker-compose.yaml file will be stored
2. Create file named `docker-compose.yaml` (this is by convention) with following content:
```
log:
    image: elasticsearch
    ports:
        - "9200:9200"
        - "9300:9300"
    environment:
        - Des.node.name="docker-local"
    restart: always
kibana:
    image: kibana
    ports:
        - "5601:5601"
    links:
        - log:elasticsearch
    restart: always
curator_with_cron:
    build: .
    command: "--host elasticsearch --port 9200 delete indices --older-than 10 --timestring '%Y.%m.%d' --time-unit days"
    links:
        - log:elasticsearch
    restart: always
```
3. Create file name `Dockerfile` with followig content:
```
FROM alpine:latest
RUN apk --update --upgrade add python py-pip && \
    pip install elasticsearch-curator
COPY ./bin/ /usr/local/bin
ENTRYPOINT ["sh", "/usr/local/bin/entrypoint"]
```
4. Create file named `entrypoint` and place it in new direcotry named `bin`. The file should have following content:
```
#!/bin/sh
# save provided arguments
cat > /etc/periodic/daily/0-curator <<EOF
#!/bin/sh
/usr/bin/curator $@
EOF
chmod +x /etc/periodic/daily/0-curator
echo "Created /etc/periodic/daily/0-curator"
echo "Command: curator $@"
echo "Executing crond..."
exec crond -f
```
> Make sure that this file is UNIX formated!
5. Navigate to working directory and execute `docker-compose build`
6. Navigate to working directory and execute `docker-compose up` and `-d` for silent mode

> If you need to stop stated containers use `docker-compose stop` and to start it `docker-compose start`. Using of `docker-compose down` will **delete** all data (i.e the logs)

**Current documentation is working with:**
> Docker version 1.11.1, build 5604cbe https://github.com/docker/docker/releases
> Docker-compose version 1.7.1, build 0a9ab35 https://github.com/docker/compose/releases
> Alpine 3.3.3 https://hub.docker.com/_/alpine/
> ElasticSearch 2.3.3 https://hub.docker.com/_/elasticsearch/
> Kibana 4.5.1 https://hub.docker.com/_/kibana/
> Curator 3.5.1 https://pypi.python.org/pypi/elasticsearch-curator/3.5.1
