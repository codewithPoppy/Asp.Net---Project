import { Component, OnDestroy, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Subject, takeUntil } from 'rxjs';
import { Event } from 'src/app/shared/interfaces/event.interface';
import { ConfirmModalComponent } from '../shared/components/confirm-modal/confirm-modal.component';
import { ToastsService } from '../shared/components/toasts/toasts.service';
import { EventService } from '../shared/services/event.service';

@Component({
  selector: 'app-events',
  templateUrl: './events.component.html',
  styleUrls: ['./events.component.scss']
})
export class EventsComponent implements OnInit, OnDestroy {
  protected readonly unsubscribe$ = new Subject<void>();

  public events: Event[] = [];

  constructor(private eventService: EventService, private modalService: NgbModal,
    private toastService: ToastsService) { }

  ngOnInit(): void {
    this.eventService
      .getList()
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe({
        next: (res) => {
          this.events = res;
        },
        error: (err: Error) => this.toastService.danger("", err.message)
      });
  }

  ngOnDestroy(): void {
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }

  public onDelete(id?: number) {
    if (!id || id <= 0) return;
    this.modalService.open(ConfirmModalComponent).result.then(() => {
      this.eventService.delete(id).subscribe({
        next: () => this.ngOnInit(),
        error: (err: Error) => this.toastService.danger("", err.message)
      });
    }).catch(() => { });
  }
}
