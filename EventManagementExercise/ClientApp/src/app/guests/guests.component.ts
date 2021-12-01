import { Component, OnDestroy, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Subject, takeUntil } from 'rxjs';
import { ConfirmModalComponent } from '../shared/components/confirm-modal/confirm-modal.component';
import { ToastsService } from '../shared/components/toasts/toasts.service';
import { Guest } from '../shared/interfaces/guest.interface';
import { GuestService } from '../shared/services/guest.service';

@Component({
  selector: 'app-guests',
  templateUrl: './guests.component.html',
  styleUrls: ['./guests.component.scss']
})
export class GuestsComponent implements OnInit, OnDestroy {
  protected readonly unsubscribe$ = new Subject<void>();

  public guests: Guest[] = [];

  constructor(private guestService: GuestService, private modalService: NgbModal,
    private toastService: ToastsService) { }

  ngOnInit(): void {
    this.guestService
      .getList()
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe({
        next: (res) => {
          this.guests = res;
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
      this.guestService.delete(id).subscribe({
        next: () => this.ngOnInit(),
        error: (err: Error) => this.toastService.danger("", err.message)
      });
    }).catch(() => { });
  }
}
