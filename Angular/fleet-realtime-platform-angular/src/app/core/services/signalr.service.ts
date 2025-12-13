import { Injectable, OnDestroy } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { BehaviorSubject } from 'rxjs';
import { environment } from '../../../environments/environment';
import { TripUpdate, TripStatus } from '../../models/trip-update';
import { DriverStatusUpdate } from '../../models/driver-status-update';

export interface VehicleLocationUpdate {
  vehicleId: string;
  latitude: number;
  longitude: number;
  timestamp: string;
}

export interface DriverHeat {
  driverId: string;
  isOnline: boolean;
  currentStatus?: string | null;
  updatesCount: number;
  lastSeen?: string;
}

@Injectable({ providedIn: 'root' })
export class SignalRService{
  private baseUrl = environment.apiBaseUrl;

  private trackingConnection!: signalR.HubConnection;
  private tripsConnection!: signalR.HubConnection;
  private driversConnection!: signalR.HubConnection;
  private alertsConnection!: signalR.HubConnection;

  private vehicleSubject = new BehaviorSubject<VehicleLocationUpdate | null>(null);
  private latestTripSubject = new BehaviorSubject<TripUpdate | null>(null);
  private latestDriverSubject = new BehaviorSubject<DriverStatusUpdate | null>(null);
  private latestAlertSubject = new BehaviorSubject<any | null>(null);

  private tripTimelineSubject = new BehaviorSubject<TripUpdate[]>([]);
  private driverHeatmapSubject = new BehaviorSubject<DriverHeat[]>([]);

  vehicle$ = this.vehicleSubject.asObservable();
  latestTrip$ = this.latestTripSubject.asObservable();
  latestDriver$ = this.latestDriverSubject.asObservable();
  latestAlert$ = this.latestAlertSubject.asObservable();

  tripTimeline$ = this.tripTimelineSubject.asObservable();
  driverHeatmap$ = this.driverHeatmapSubject.asObservable();

  private readonly maxTimelineItems = 12;

  constructor() {
    this.startTracking();
    this.startTrips();
    this.startDrivers();
    this.startAlerts();
  }

  private startTracking() {
    this.trackingConnection = new signalR.HubConnectionBuilder()
      .withUrl(`${this.baseUrl}/hubs/tracking`)
      .withAutomaticReconnect()
      .build();

    this.trackingConnection.on('ReceiveLocation', (data: VehicleLocationUpdate) => {
      data.timestamp = data.timestamp ?? new Date().toISOString();
      this.vehicleSubject.next(data);
    });

    this.startConnection(this.trackingConnection, 'TrackingHub');
  }

  private startTrips() {
    this.tripsConnection = new signalR.HubConnectionBuilder()
      .withUrl(`${this.baseUrl}/hubs/trips`)
      .withAutomaticReconnect()
      .build();

    this.tripsConnection.on('TripUpdated', (data: TripUpdate) => {
      data.timestamp = data.timestamp ?? new Date().toISOString();
      this.latestTripSubject.next(data);

      const current = this.tripTimelineSubject.value;
      const updated = [data, ...current].slice(0, this.maxTimelineItems);
      this.tripTimelineSubject.next(updated);
    });

    this.startConnection(this.tripsConnection, 'TripsHub');
  }

  private startDrivers() {
    this.driversConnection = new signalR.HubConnectionBuilder()
      .withUrl(`${this.baseUrl}/hubs/drivers`)
      .withAutomaticReconnect()
      .build();

    this.driversConnection.on('DriverStatusChanged', (data: DriverStatusUpdate) => {
      data.timestamp = data.timestamp ?? new Date().toISOString();
      this.latestDriverSubject.next(data);

      const heat = [...this.driverHeatmapSubject.value];
      const existingIndex = heat.findIndex(h => h.driverId === data.driverId);

      if (existingIndex >= 0) {
        const current = heat[existingIndex];
        heat[existingIndex] = {
          ...current,
          isOnline: data.isOnline,
          currentStatus: data.currentStatus,
          lastSeen: data.timestamp,
          updatesCount: current.updatesCount + 1
        };
      } else {
        heat.push({
          driverId: data.driverId,
          isOnline: data.isOnline,
          currentStatus: data.currentStatus,
          lastSeen: data.timestamp,
          updatesCount: 1
        });
      }

      this.driverHeatmapSubject.next(heat);
    });

    this.startConnection(this.driversConnection, 'DriversHub');
  }

  private startAlerts() {
    this.alertsConnection = new signalR.HubConnectionBuilder()
      .withUrl(`${this.baseUrl}/hubs/alerts`)
      .withAutomaticReconnect()
      .build();

    this.alertsConnection.on('AlertPushed', alert => {
      alert.createdAt = alert.createdAt ?? new Date().toISOString();
      this.latestAlertSubject.next(alert);
    });

    this.startConnection(this.alertsConnection, 'AlertsHub');
  }

  private startConnection(connection: signalR.HubConnection, name: string) {
    connection
      .start()
      .then(() => console.log(`${name} connected`))
      .catch(err => {
        console.error(`${name} connection error:`, err);
        setTimeout(() => this.startConnection(connection, name), 5000);
      });
  }
}
