import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { GuestsComponent } from './guests/guests.component';
import { HeaderComponent } from './layout/header/header.component';
import { HomeComponent } from './home/home.component';
import { FooterComponent } from './layout/footer/footer.component';
import { ToastContainerComponent } from './shared/components/toast-container/toast-container.component';
import { HttpClientModule } from '@angular/common/http';
import { GuestFormComponent } from './guests/guest-form/guest-form.component';
import { TagInputModule } from 'ngx-chips';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ConfirmModalComponent } from './shared/components/confirm-modal/confirm-modal.component';
import { EventsComponent } from './events/events.component';
import { EventFormComponent } from './events/event-form/event-form.component';

@NgModule({
  declarations: [
    AppComponent,
    GuestsComponent,
    HeaderComponent,
    HomeComponent,
    FooterComponent,
    ToastContainerComponent,
    GuestFormComponent,
    ConfirmModalComponent,
    EventsComponent,
    EventFormComponent,
  ],
  imports: [
    FormsModule,
    ReactiveFormsModule,
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    NgbModule,
    HttpClientModule,
    TagInputModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
