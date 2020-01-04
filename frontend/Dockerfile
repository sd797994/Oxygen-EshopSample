FROM node:12.13.1-slim AS build
WORKDIR /src
COPY . .
RUN npm install
RUN npm run build
FROM nginx:latest AS base
WORKDIR /app
COPY --from=build /src/dist /usr/share/nginx/html/