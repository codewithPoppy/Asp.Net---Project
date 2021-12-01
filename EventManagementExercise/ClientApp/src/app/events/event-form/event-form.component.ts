import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { Location } from "@angular/common";
import { NgForm } from '@angular/forms';
import { NgbDateStruct } from '@ng-bootstrap/ng-bootstrap';
import { Observable, Subject, takeUntil } from 'rxjs';
import { Event } from 'src/app/shared/interfaces/event.interface';
import { Guest } from 'src/app/shared/interfaces/guest.interface';
import { EventService } from 'src/app/shared/services/event.service';
import { ToastsService } from 'src/app/shared/components/toasts/toasts.service';
import { GuestService } from 'src/app/shared/services/guest.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-event-form',
  templateUrl: './event-form.component.html',
  styleUrls: ['./event-form.component.scss']
})
export class EventFormComponent implements OnInit, OnDestroy {
  protected readonly unsubscribe$ = new Subject<void>();

  @ViewChild("editForm") editForm!: NgForm;

  public minDate: Date = new Date();
  public minDateStruct: NgbDateStruct = {
    year: this.minDate.getFullYear(),
    month: this.minDate.getMonth() + 1,
    day: this.minDate.getDate()
  };
  public event: Event = { name: "", date: this.minDate, guests: [] };
  public dobModel: NgbDateStruct = this.minDateStruct;
  public guests: Guest[] = [];

  constructor(private _location: Location, private eventService: EventService,
    private guestService: GuestService, private toastService: ToastsService,
    private router: Router, private route: ActivatedRoute,) { }

  ngOnInit(): void {
    // guests
    this.guestService.getList().pipe(takeUntil(this.unsubscribe$))
      .subscribe({ next: (res) => this.guests = res });

    // event
    const id: number = parseInt(this.route.snapshot.paramMap.get("id") || "");
    if (id > 0) {
      this.eventService
        .get(id)
        .pipe(takeUntil(this.unsubscribe$))
        .subscribe({
          next: (res) => {
            this.event = res;
            const date: Date = new Date(typeof this.event.date === 'string' ?
              Date.parse(this.event.date) : this.event.date);
            this.dobModel = {
              year: date.getFullYear(),
              month: date.getMonth() + 1,
              day: date.getDate(),
            };
          },
          error: (err: Error) => this.toastService.danger("", err.message)
        });
    }
  }

  ngOnDestroy(): void {
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }

  public isChecked(guestId: number) {
    return this.event.guests?.some(guest => {
      if (typeof guest === 'number') return guest === guestId;
      return guest.id === guestId;
    });
  }

  public onGuestsChanged(guestId: number) {
    let index = this.event.guests?.findIndex(guest => {
      if (typeof guest === 'number') return guest === guestId;
      return guest.id === guestId;
    })
    if (index === undefined || index == -1) (this.event.guests as number[]).push(guestId);
    else this.event.guests?.splice(index, 1);
  }

  public onSubmit() {
    // date
    let date: Date = new Date(this.dobModel.year, this.dobModel.month - 1);
    date.setUTCDate(this.dobModel.day);
    this.event.date = date;

    // guests
    this.event.guests = this.event.guests?.map(guest => typeof guest === 'number' ? guest : guest.id) as number[];

    let observable = new Observable<boolean>();
    observable = this.event.id ? this.eventService.update(this.event) : this.eventService.create(this.event);
    observable.pipe(takeUntil(this.unsubscribe$)).subscribe({
      next: () => {
        this.toastService.success("", "Event registered.");
        this.router.navigateByUrl(`events`);
      },
      error: (err: Error) => this.toastService.danger("", err.message)
    });
  }

  public goBack() {
    this._location.back();
  }
}
