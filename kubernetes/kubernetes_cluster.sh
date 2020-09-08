#!/bin/sh
# A Simple Shell Script To Build a Kubernetes Cluster with Kubeadm
# Dahlan Faroga Sirait - 09/Sept/2020

# Set execute permission on your script:
# chmod +x setup.sh

# How to run .sh file as root user
# sudo bash setup.sh

# SCP (secure copy) is a command-line utility that allows you to securely copy files and directories between two locations
# scp [OPTION] [user@]SRC_HOST:]file1 [user@]DEST_HOST:]file2


# A. Install Docker on all three nodes

# 1. Add the Docker GPG key
curl -fsSL https://download.docker.com/linux/ubuntu/gpg | apt-key add -

# 2. Add the Docker repository
add-apt-repository "deb [arch=amd64] https://download.docker.com/linux/ubuntu $(lsb_release -cs) stable"

# 3. Update packages
apt-get update

# 4. Install Docker
apt-get install -y docker-ce=18.06.1~ce~3-0~ubuntu

# 5. Hold Docker at this specific version
apt-mark hold docker-ce

# B. Install Kubeadm, Kubelet, and Kubectl on all three nodes

# 1. Add the Kubernetes GPG key
curl -s https://packages.cloud.google.com/apt/doc/apt-key.gpg | sudo apt-key add -

# 2. Add the Kubernetes repository
cat << EOF | tee /etc/apt/sources.list.d/kubernetes.list
deb https://apt.kubernetes.io/ kubernetes-xenial main
EOF

# 3. Update packages
apt-get update 

# 4. Install kubelet, kubeadm, and kubectl 
apt-get install -y kubelet=1.14.5-00 kubeadm=1.14.5-00 kubectl=1.14.5-00

# 5. Hold the Kubernetes components at this specific version
apt-mark hold kubelet kubeadm kubectl
 
# A.6. Verify that Docker is up and running
systemctl status docker



# C. Bootstrap the cluster on the Kube master node

# 1. On the Kube master node
#  sudo kubeadm init --pod-network-cidr=10.244.0.0/16

# Take note that the kubeadm init command printed a long kubeadm join command to the screen. 
# You will need that kubeadm join command in the next step!

# That command may take a few minutes to complete. 
# 2. When it is done, set up the local kubeconfig
#  mkdir -p $HOME/.kube
#  sudo cp -i /etc/kubernetes/admin.conf $HOME/.kube/config
#  sudo chown $(id -u):$(id -g) $HOME/.kube/config

# 3. verify it is up and running
#  kubectl version
# This command should return both a Client Version and a Server Version.



# D. Join the two Kube worker nodes to the cluster

# 1. Copy the kubeadm join command that was printed by the kubeadm init command earlier, with the token and hash. 
# Run this command on both worker nodes, but make sure you add sudo in front of it
#  sudo kubeadm join $some_ip:6443 --token $some_token --discovery-token-ca-cert-hash $some_hash

# 2. Now, on the Kube master node, make sure your nodes joined the cluster successfully
#  kubectl get nodes
# Note that the nodes are expected to be in the NotReady state for now


# E. Set up cluster networking with flannel

# 1. Turn on iptables bridge calls on all three nodes
#  echo "net.bridge.bridge-nf-call-iptables=1" | sudo tee -a /etc/sysctl.conf
#  sudo sysctl -p

# 2. Next, run this only on the Kube master node
#  kubectl apply -f https://raw.githubusercontent.com/coreos/flannel/bc79dd1505b0c8681ece4de4c0d86c5cd2643275/Documentation/kube-flannel.yml

# 3. Verify status
#  kubectl get nodes
# All three nodes should be in the Ready state
