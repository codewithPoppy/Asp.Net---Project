import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { Location } from "@angular/common";
import { Guest } from 'src/app/shared/interfaces/guest.interface';
import { NgbDateStruct } from '@ng-bootstrap/ng-bootstrap';
import { Allergy } from 'src/app/shared/interfaces/allergy.interface';
import { AllergyService } from 'src/app/shared/services/allergy.service';
import { Observable, Subject, takeUntil } from 'rxjs';
import { ToastsService } from 'src/app/shared/components/toasts/toasts.service';
import { NgForm } from '@angular/forms';
import { GuestService } from 'src/app/shared/services/guest.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-guest-form',
  templateUrl: './guest-form.component.html',
  styleUrls: ['./guest-form.component.scss']
})
export class GuestFormComponent implements OnInit, OnDestroy {
  protected readonly unsubscribe$ = new Subject<void>();

  @ViewChild("editForm") editForm!: NgForm;

  public guest: Guest = { firstName: "", lastName: "", email: "", allergies: [] };
  public dobModel: NgbDateStruct = { year: 1990, month: 6, day: 15 };
  public allergies: Allergy[] = [];

  constructor(private _location: Location, private allergyService: AllergyService,
    private guestService: GuestService, private toastService: ToastsService,
    private router: Router, private route: ActivatedRoute,) { }

  ngOnInit(): void {
    // allergies
    this.allergyService.getList().pipe(takeUntil(this.unsubscribe$))
      .subscribe({ next: (res) => this.allergies = res });

    // guest
    const id: number = parseInt(this.route.snapshot.paramMap.get("id") || "");
    if (id > 0) {
      this.guestService
        .get(id)
        .pipe(takeUntil(this.unsubscribe$))
        .subscribe({
          next: (res) => {
            this.guest = res;
            if (this.guest.dob) {
              const date: Date = new Date(typeof this.guest.dob === 'string' ?
                Date.parse(this.guest.dob) : this.guest.dob);
              this.dobModel = {
                year: date.getFullYear(),
                month: date.getMonth() + 1,
                day: date.getDate(),
              };
            }
          },
          error: (err: Error) => this.toastService.danger("", err.message)
        });
    }
  }

  ngOnDestroy(): void {
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }

  public onSubmit() {
    // dob
    let date: Date = new Date(this.dobModel.year, this.dobModel.month - 1);
    date.setUTCDate(this.dobModel.day);
    this.guest.dob = date.toISOString();

    let observable = new Observable<boolean>();
    observable = this.guest.id ? this.guestService.update(this.guest) : this.guestService.create(this.guest);
    observable.pipe(takeUntil(this.unsubscribe$)).subscribe({
      next: () => {
        this.toastService.success("", "Guest registered.");
        this.router.navigateByUrl(`guests`);
      },
      error: (err: Error) => this.toastService.danger("", err.message)
    });
  }

  public goBack() {
    this._location.back();
  }
}
