import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EventFormComponent } from './events/event-form/event-form.component';
import { EventsComponent } from './events/events.component';
import { GuestFormComponent } from './guests/guest-form/guest-form.component';
import { GuestsComponent } from './guests/guests.component';
import { HomeComponent } from './home/home.component';

const routes: Routes = [
  { path: '', component: HomeComponent, pathMatch: 'full' },
  { path: 'guests', component: GuestsComponent },
  { path: 'guests/add', component: GuestFormComponent },
  { path: 'guests/edit/:id', component: GuestFormComponent },
  { path: 'events', component: EventsComponent },
  { path: 'events/add', component: EventFormComponent },
  { path: 'events/edit/:id', component: EventFormComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
