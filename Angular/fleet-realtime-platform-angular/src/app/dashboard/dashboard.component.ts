import { Component, DestroyRef, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Observable } from 'rxjs';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';

import { SignalRService, DriverHeat } from '../core/services/signalr.service';
import { TripUpdate, TripStatus } from '../models/trip-update';
import { DriverStatusUpdate } from '../models/driver-status-update';
import { VehicleLocationUpdate } from '../core/services/signalr.service';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent {

  private destroyRef = inject(DestroyRef);

  vehicle$!: Observable<VehicleLocationUpdate | null>;
  latestTrip$!: Observable<TripUpdate | null>;
  latestDriver$!: Observable<DriverStatusUpdate | null>;
  latestAlert$!: Observable<any | null>;

  tripTimeline$!: Observable<TripUpdate[]>;
  driverHeatmap$!: Observable<DriverHeat[]>;

  stats = {
    vehicles: 0,
    activeTrips: 0,
    driversOnline: 0,
    alerts: 0
  };

  constructor(private signalR: SignalRService) {

    this.vehicle$ = this.signalR.vehicle$;
    this.latestTrip$ = this.signalR.latestTrip$;
    this.latestDriver$ = this.signalR.latestDriver$;
    this.latestAlert$ = this.signalR.latestAlert$;

    this.tripTimeline$ = this.signalR.tripTimeline$;
    this.driverHeatmap$ = this.signalR.driverHeatmap$;

    this.vehicle$
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe(v => v && this.stats.vehicles++);

    this.latestTrip$
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe(t => {
        if (!t) return;
        if (t.status === TripStatus.InProgress) this.stats.activeTrips++;
      });

    this.driverHeatmap$
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe(heat => {
        this.stats.driversOnline = heat.filter(d => d.isOnline).length;
      });

    this.latestAlert$
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe(a => a && this.stats.alerts++);
  }

  getStatusLabel(status: TripStatus): string {
    switch (status) {
      case TripStatus.Created: return 'Created';
      case TripStatus.Assigned: return 'Assigned';
      case TripStatus.InProgress: return 'In Progress';
      case TripStatus.Completed: return 'Completed';
      case TripStatus.Cancelled: return 'Cancelled';
      default: return 'Unknown';
    }
  }

  getStatusClass(status: TripStatus): string {
    switch (status) {
      case TripStatus.InProgress: return 'badge badge--in-progress';
      case TripStatus.Completed: return 'badge badge--completed';
      case TripStatus.Cancelled: return 'badge badge--cancelled';
      case TripStatus.Assigned: return 'badge badge--assigned';
      default: return 'badge';
    }
  }

  getHeatClass(driver: DriverHeat): string {
    if (driver.updatesCount > 10) return 'heat heat--high';
    if (driver.updatesCount > 5) return 'heat heat--medium';
    return 'heat heat--low';
  }
}
