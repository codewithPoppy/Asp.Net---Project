import { Component, OnInit } from '@angular/core';
import { ToastsService } from '../toasts/toasts.service';

@Component({
  selector: 'app-toast-container',
  templateUrl: './toast-container.component.html',
  styleUrls: ['./toast-container.component.scss']
})
export class ToastContainerComponent implements OnInit {

  constructor(public toastService: ToastsService) { }

  ngOnInit(): void { }

}
