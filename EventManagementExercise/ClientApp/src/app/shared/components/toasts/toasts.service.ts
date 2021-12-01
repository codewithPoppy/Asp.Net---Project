import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ToastsService {
  toasts: Toast[] = [];

  constructor() { }

  public show(header: string, body: string) {
    this.toasts.push({ header, body });
  }

  public success(header: string, body: string) {
    this.toasts.push({ header, body, classname: 'bg-success text-light' });
  }

  public danger(header: string, body: string) {
    this.toasts.push({ header, body, classname: 'bg-danger text-light' });
  }

  public warning(header: string, body: string) {
    this.toasts.push({ header, body, classname: 'bg-warning text-light' });
  }

  public remove(toast: any) {
    this.toasts = this.toasts.filter(t => t != toast);
  }
}

export interface Toast {
  header: string;
  body: string;
  delay?: number;
  classname?: string;
}