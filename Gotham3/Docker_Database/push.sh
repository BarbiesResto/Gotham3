echo "$DOCKER_PASSWORD" | docker login -u "$DOCKER_USERNAME" --password-stdin

docker push barbiesresto/gotham3:db
docker push barbiesresto/gotham3:app