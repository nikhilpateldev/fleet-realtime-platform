export interface DriverStatusUpdate {
  driverId: string;
  isOnline: boolean;
  currentStatus?: string | null;
  timestamp?: string;
}
