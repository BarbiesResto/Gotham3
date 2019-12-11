echo "$DOCKER_PASSWORD" | docker login -u "$DOCKER_USERNAME" --password-stdin

docker push barbiesresto/Gotham3:db
docker push barbiesresto/Gotham3:app