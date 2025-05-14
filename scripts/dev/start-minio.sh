#!/bin/sh

# start MinIO server in background
minio $MINIO_COMMAND &

# wait for it to be in a ready state
until [ "$(curl -s -o /dev/null -w "%{http_code}" http://localhost:$MINIO_PORT/minio/health/ready)" = "200" ]; do
  echo "Waiting for MinIO service..."
  sleep 1
done

# create buckets if necessary
mc alias set cloudstates http://localhost:$MINIO_PORT $MINIO_ROOT_USER $MINIO_ROOT_PASSWORD
if ! mc ls cloudstates/$MINIO_SAVESTATES_BUCKET > /dev/null 2>&1; then
  echo "Creating savestates bucket."
  mc mb cloudstates/$MINIO_SAVESTATES_BUCKET
  mc anonymous set public cloudstates/$MINIO_SAVESTATES_BUCKET
fi

wait