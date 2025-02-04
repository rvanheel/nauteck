docker build --pull --rm -f "nauteck.web/Dockerfile" -t cloud4u:nauteck-dealer "."
docker tag cloud4u:nauteck-dealer registry.digitalocean.com/cloud4u/cloud4u:nauteck-dealer
docker push registry.digitalocean.com/cloud4u/cloud4u:nauteck-dealer