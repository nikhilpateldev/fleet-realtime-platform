export enum TripStatus {
  Created = 0,
  Assigned = 1,
  InProgress = 2,
  Completed = 3,
  Cancelled = 4
}

export interface TripUpdate {
  tripId: string;
  status: TripStatus;
  timestamp: string;
}
