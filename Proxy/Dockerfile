FROM nginx
COPY ["./conf/default.conf","/etc/nginx/conf.d/default.conf"]

# Copy the .htpasswd file (which contains your Basic Auth users)
COPY ./.htpasswd /etc/nginx/.htpasswd