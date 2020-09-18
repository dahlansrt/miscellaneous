#!/bin/sh
# A Simple Shell Script To Build a Kubernetes Cluster with Kubeadm
# Dahlan Faroga Sirait - 18/Sept/2020

# Set execute permission on your script:
# chmod +x setup.sh

# How to run .sh file as root user
# sudo bash setup.sh

# SCP (secure copy) is a command-line utility that allows you to securely copy files and directories between two locations
# scp [OPTION] [user@]SRC_HOST:]file1 [user@]DEST_HOST:]file2

# A. Install Docker & Kubernetes on all nodes

# 1. Disable SELinux
setenforce 0
sed -i --follow-symlinks 's/SELINUX=enforcing/SELINUX=disabled/g' /etc/sysconfig/selinux

# 2. Enable the br_netfilter module for cluster communication
modprobe br_netfilter
echo '1' > /proc/sys/net/bridge/bridge-nf-call-iptables

# 3. Ensure that the Docker dependencies are satisfied
yum install -y yum-utils device-mapper-persistent-data lvm2

# 4. Add the Docker repo and install Docker
yum-config-manager --add-repo https://download.docker.com/linux/centos/docker-ce.repo
yum install -y docker-ce

# 5. Set the cgroup driver for Docker to systemd, reload systemd, then enable & start Docker
sed -i '/^ExecStart/ s/$/ --exec-opt native.cgroupdriver=systemd/' /usr/lib/systemd/system/docker.service
systemctl daemon-reload
systemctl enable docker --now

# 6. Add the Kubernetes repo
cat << EOF > /etc/yum.repos.d/kubernetes.repo
[kubernetes]
name=Kubernetes
baseurl=https://packages.cloud.google.com/yum/repos/kubernetes-el7-x86_64
enabled=1
gpgcheck=0
repo_gpgcheck=0
gpgkey=https://packages.cloud.google.com/yum/doc/yum-key.gpg
https://packages.cloud.google.com/yum/doc/rpm-package-key.gpg
EOF

# 7. Install Kubernetes
yum install -y kubelet kubeadm kubectl

# 8. Enable the kubelet service
#    The kubelet service will fail to start until the cluster is initialized
systemctl enable kubelet

# MASTER Node

# 1. Initialize the cluster using the IP range for Flannel. Copy the [kubeadm join] command
# kubeadm init --pod-network-cidr=10.244.0.0/16

# 2. Exit sudo, copy the admin.conf to your home directory, and take ownership
# mkdir -p $HOME/.kube
# sudo cp -i /etc/kubernetes/admin.conf $HOME/.kube/config
# sudo chown $(id -u):$(id -g) $HOME/.kube/config

# 3. Deploy Flannel
# kubectl apply -f https://raw.githubusercontent.com/coreos/flannel/master/Documentation/kube-flannel.yml

# 4. Check the cluster state
# kubectl get pods --all-namespaces

# 5. After join command on WORKER nodes
# kubectl get nodes

# WORKER Nodes
# 1. Run the join command

# MASTER Node: CREATE AND SCALE A DEPLOYMENT USING kubectl

# 1. Create a simple deployment
# kubectl create deployment nginx --image=nginx

# 2. Inspect the pod
# kubectl get pods

# 3. Scale the deployment
# kubectl scale deployment nginx --replicas=4

# 4. Inspect the pods. Should have 4
# kubectl get pods
