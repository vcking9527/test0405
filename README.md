
## 設定環境

```bash
# 安裝docker - 省略
# 安裝dotnet core 6 - 省略
# 安裝node.js - 省略

# 安裝docker-compose
$ sudo curl -L "https://github.com/docker/compose/releases/download/1.26.2/docker-compose-$(uname -s)-$(uname -m)" -o /usr/local/bin/docker-compose

# 下載後檢查執行狀態
$ ls -al /usr/local/bin/docker-compose

# 將docker-compose設定為可執行檔案
$ sudo chmod +x /usr/local/bin/docker-compose

# 檢查設定完成結果
$ ls -al /usr/local/bin/docker-compose

# 檢查版本
$ docker-compose --version
```

## 建置Infra環境

> 可以參考[原始Repo](https://github.com/deviantony/docker-elk/tree/release-7.x)

```bash
# 切換到docker目錄底下
$ cd script/docker 

# 啟動服務，建立Elasticsearch、Kibana
docker-compose up -d

# 檢查服務狀態
docker-compose ps

# 確認elasticsearch有安裝好
curl -v http://localhost:9200

# 打開瀏覽器，登入Kibana。打帳號和密碼為 elastic / changeme
```

## 基本Kibana操作KQL

```bash
# 查詢index指令，看有哪些可以使用
GET _cat/indices?v

# 餵入script/accounts.json的資料
PUT /accounts/_bulk
{"index":{"_id":"1"}}
{"account_number":1,"balance":39225,"firstname":"Amber","lastname":"Duke","age":32,"gender":"M","address":"880 Holmes Lane","employer":"Pyrami","email":"amberduke@pyrami.com","city":"Brogan","state":"IL"}

# 查詢index為accounts
POST accounts/_search

```

## 建置應用程式

### 建置前端
> 請確保你已經安裝npm 16以上的版本，請確保你在根目錄

```bash
# 檢查路徑
$ pwd
/home/xxxxxx/ELK-Demo

# 切換到前端專案
cd src/client-app

# 安裝node_module
npm install

# 啟動服務，使用port 3000，開啟網站http://localhost:3000，預設是8080 port
npm run serve -- --port 3000
```

### 建置後端
> 請確保你已經安裝.net core 6以上，請確保你在根目錄

```bash
# 檢查路徑
$ pwd
/home/xxxxxx/ELK-Demo

# 切換到後端專案
cd src/ELK.Demo.WebApi

# 恢復.net core專案，會去載nuget
dotnet restore

# 運行熱更新
dotnet watch run

```

### 透過Dockerfile建立應用程式

```bash
# 檢查路徑
$ pwd
/home/xxxxxx/ELK-Demo

# 建置docker images
docker build -t elk-demo .

# 移除不使用的multi-stage殘留物 (optional)
docker rmi -f $(docker images -q --filter label=stage=intermediate)

# 啟動服務，ElasticSettings__baseUrl可以替換成公司的ES網址
docker run -d --name elk-demo -p 5001:5001 -e ElasticSettings__baseUrl="http://localhost:9200/" elk-demo:latest
```

## Troubleshooting

### 安裝vue-echarts時，出現依賴衝突的問題

```bash
npm install --legacy-peer-deps echarts vue-echarts

npm i -D --legacy-peer-deps @vue/composition-api

npm i -D --legacy-peer-deps @types/echarts

```