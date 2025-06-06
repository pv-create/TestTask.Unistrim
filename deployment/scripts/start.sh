#!/bin/bash

echo "Starting monitoring stack..."

# Создание необходимых директорий
mkdir -p data/prometheus
mkdir -p data/grafana

# Установка прав доступа
sudo chown -R 472:472 data/grafana
sudo chown -R 65534:65534 data/prometheus

# Запуск всех сервисов
docker-compose up -d

echo "Waiting for services to start..."
sleep 30

echo "Services status:"
docker-compose ps

echo ""
echo "Access URLs:"
echo "Application: http://localhost:5000"
echo "Prometheus: http://localhost:9090"
echo "Grafana: http://localhost:3000 (admin/admin)"