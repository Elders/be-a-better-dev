#Docker with docker compose hosted in Azure CoreOS VM#

1. SSH to the VM (ssh {user}@{ipAddress})
2. Execute `sudo su -` in order to enter root mode
3. Execute `systemctl enable docker` in order Docker to start at system boot. More information https://docs.docker.com/engine/admin/systemd/
4. Execute `mkdir /opt/` and `mkdir /opt/bin` to create the necessary writable directories for installation of docker-compose
5. Download/install docker-compose ```curl -L https://github.com/docker/compose/releases/download/{VERSION}/docker-compose-`uname -s`-`uname -m` > /opt/bin/docker-compose```. Replace {VERSION} with latest available from https://github.com/docker/compose/releases
> Note: If you try to execute above command with (`/usr/local/bin/docker-compose`) you won't have permitions and following error will occur `-bash: /usr/local/bin/docker-compose: Read-only file system`
6. Execute `chmod +x /opt/bin/docker-compose` in order to give docker-compose binary executable rights
7. Create directory where docker related files (docker-compose.yaml, dockerfile etc) will be stored
> ex: execute `mkdir /opt/docker-compose` and `mkdir /opt/docker-compose/log`
8. Execute `chmod +x {FILE_PATH}` and `chown {user} {FILE_PATH}`
> ex: execute `chmod +x /opt/docker-compose/log` and `chown elders /opt/docker-compose/log`
9. Copy docker-compose.yaml (and Dockerfile if necessary) to the remote machine by executing `scp {PATH_SOURCE} {user}@{ipAddress}:{PATH_DESTINATION}`. To copy all files including sub directories use `scp -r * @{ipAddress}:{PATH_DESTINATION}`
> ex: scp -r * elders@1.2.3.4:/opt/docker-compose/log
10. Navigate to docker-compose.yaml file in remote machine and execute `docker-compose build` to build composed docker image/images
11. Execute `docker-compose up` to stat builded image/images

**Current documentation is working with:**

> CoreOS 4.5.0-coreos-r1 https://coreos.com/releases/
>
> Docker version 1.10.3, build 8acee1b https://github.com/docker/docker/releases
> 
> Docker-compose version 1.7.1, build 0a9ab35 https://github.com/docker/compose/releases