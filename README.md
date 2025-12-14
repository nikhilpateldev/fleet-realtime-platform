# 🚚 Fleet Realtime Platform

**Enterprise grade real time fleet operations platform built with Angular 18 (Standalone), ASP.NET Core, and SignalR.**  
Live vehicle tracking • Animated trip timelines • Driver availability heatmaps • Realtime alerts • Event driven architecture

---

<img width="3793" height="1863" alt="RealTime_Dashboard" src="https://github.com/user-attachments/assets/37907901-d4d4-452c-b206-aa3f774d59b5" />

---

## 🌐 Overview

Fleet Realtime Platform is a **full stack, real time fleet monitoring system** designed to simulate modern logistics and transportation operations.  
It demonstrates how large scale systems stream live data using **SignalR over WebSockets** and visualize it using an **Angular 18 standalone dashboard**.

This project is ideal for:

- ✅ Enterprise real time system demos  
- ✅ Event driven architecture learning  
- ✅ Full stack .NET + Angular portfolios  
- ✅ WebSocket / SignalR streaming showcases  

---

## 🧱 Tech Stack

### Frontend
- Angular 18 (Standalone Architecture)
- TypeScript
- RxJS
- SignalR JavaScript Client
- SCSS

### Backend
- ASP.NET Core Web API (.NET 8)
- SignalR Hubs (WebSockets)
- Background Worker for realtime simulation
- Clean Architecture (Domain, Application, Infrastructure, API)
- Entity Framework Core (optional DB mode)

---

## ⚡ Core Features

- 🚚 **Live Vehicle Tracking**
- 🎞️ **Realtime Trip Timeline Animations**
- 🗺️ **Driver Availability Heatmap**
- 🚨 **Realtime Alerts Panel**
- 📊 **Live KPI Counters**
- 🔐 **JWT-ready SignalR Authorization (extensible)**
- 🧠 **Event-Driven Architecture**
- 🌍 **Angular 18 Standalone Architecture**
- 🧪 **Background Data Simulator**

---

## 📂 Project Structure

```
fleet-realtime-platform/
│
├── backend/
│   ├── Fleet.API/
│   ├── Fleet.Application/
│   ├── Fleet.Domain/
│   └── Fleet.Infrastructure/
│
└── frontend/
    └── fleet-standalone-dashboard/
```

---

## 🚀 Getting Started

### ✅ Backend (.NET)

```bash
cd backend/Fleet.API
dotnet restore
dotnet run
```

SignalR runs at:

```
https://localhost:5001/hubs/
```

---

### ✅ Frontend (Angular 18)

```bash
cd frontend/fleet-standalone-dashboard
npm install
ng serve
```

Open:

```
http://localhost:4200
```

---

## 🔁 Realtime Data Simulation

This project includes a **background SignalR simulator** that continuously pushes:

- Vehicle GPS coordinates  
- Driver online/offline states  
- Trip lifecycle status changes  
- Maintenance/system alerts  

No manual API triggering is required for demo mode.

---

## 🔐 Security

- Token based authentication ready
- SignalR supports JWT headers
- Role based hub authorization supported

---

## 📦 Deployment Ready

- Docker ready backend
- Docker ready frontend
- Cloud deployable (AWS / Azure / GCP)
- SignalR scale out ready (Redis backplane supported)

---

## 👨‍💻 Author

**Nikhil Patel**  
Senior Full Stack Engineer | .NET | Angular | Cloud | AI  

---

## ⭐ Support

If you find this project useful, please give it a ⭐ on GitHub and share it with the community!

