#!/bin/bash

echo "Starting load test..."

BASE_URL="http://localhost:5000"

# Функция для генерации нагрузки
generate_load() {
    for i in {1..1000}; do
        curl -s "$BASE_URL/api/weather" > /dev/null &
        if [ $((i % 10)) -eq 0 ]; then
            echo "Sent $i requests"
            sleep 0.1
        fi
    done
    wait
}

# Запуск нескольких потоков
for j in {1..5}; do
    generate_load &
done

wait
echo "Load test completed!"