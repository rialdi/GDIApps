# Adding SSH Keys to Droplets

## How it Works

You can add SSH keys to DigitalOcean which can then be selected during the droplet create process to add the selected SSH keys under the root user.

When using SSH keys a root password will no longer be set as SSH keys will be used as the preferred method of access.

We do not manage the server after creation, so editing, adding, or removing SSH keys from the SSH interface will not affect any of the stored keys on droplets that you have created.

Step 1: Check if an SSH keys exist

$ cd ~/.ssh
$ ls *.pub
Step 2: Generate a new SSH key

$ ssh-keygen -t rsa -C "email@example.com"
Generating public/private rsa key pair.
Enter file in which to save the key (/Users/you/.ssh/id_rsa): [Press enter]
Here you can optionally enter in a passphrase which will provide an added layer of security. Whenever you use the SSH key for access you will be prompted to enter it, otherwise you can leave this blank.

Enter passphrase (empty for no passphrase): [Type a passphrase]
Enter same passphrase again: [Type passphrase again]
Now your SSH key will be generated.

Your identification has been saved in /Users/your_username/.ssh/id_rsa.
Your public key has been saved in /Users/your_username/.ssh/id_rsa.pub.
The key fingerprint is:
01:0f:f4:3b:ca:85:d6:17:a1:7d:f0:68:9d:f0:a2:db email@example.com
Step 3: Add your SSH key to DigitalOcean

Now you are ready to add your SSH key to DigitalOcean. Simply click Add SSH Key at the top of this page and copy the contents of your key:

$ cat ~/.ssh/id_rsa.pub
Step 4: Create a droplet with SSH keys

Now you are ready to create a droplet with your SSH key available during the creation process. When you create the new droplet simply select the key and it will be automatically added to the root user of your droplet. Then you can SSH directly to the server as normal:

$ ssh root@xx.xx.xx.xx


https://www.digitalocean.com/community/tutorials/how-to-set-up-ssh-keys-on-ubuntu-22-04

https://github.com/appleboy/scp-action#setting-up-a-ssh-key

# rsa
cat .ssh/id_rsa.pub | ssh b@B 'cat >> .ssh/authorized_keys'

# d25519
cat .ssh/id_ed25519.pub | ssh b@B 'cat >> .ssh/authorized_keys'

