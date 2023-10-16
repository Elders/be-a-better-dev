## Set up github SSH key


### If you don't have any keys configured on your machine follow these steps:

0. Make sure you have installed git

1. Open git bash and type 
    
    ```
    ssh-keygen -t ed25519 -C "your_email@domain.com"
    ```

    NB! note that this will create the ssh key with the default name "id_ed25519" and you wouldn't need to configure it later , if you want a different name -> see steps for configuring multiple ssh keys. 

2. When promted to choose a file location click Enter for the default location (.ssh folder)
3. You can either choose a password or skip it by clicking Enter again.
4. Next step is to add your key to the ssh agent. Open a terminal and type:
    ```
    start-ssh-agent
    ```
    This will automatically start the agent and add your key.

5. On your git hub page and click "new ssh key" , select authentication key ,paste the contents of your id_ed25519.pub public key.

6. Open a terminal and type
    ```
    ssh -T git@github.com
    ```

    This will create a known_hosts file add github.com to it. You are all set!

### Steps for configuring multiple ssh keys

1. Follow steps 1-5 from the above tutorial, but make sure to replace the name of your key with a new one. Now if you try pulling from remote, you will likely get this error: "Permission denied (publickey)."
2. If there is no config file not present in the .ssh folder (path : C:\Users\YourUser\.ssh) , open a new linux terminal in the folder directory and execute the following command:
    ```
    touch config
    ```

3. Open the file with an editor of your choice and paste the following configuration:
    ```
    Host github.com
      HostName github.com
      IgnoreUnknown UseKeychain
      AddKeysToAgent yes
      UseKeychain yes
      IdentityFile ~/.ssh/replace_with_the_name_of_your_key
    ```

    You can do the same with other hosts like gitlab for example. That is it you are all done!