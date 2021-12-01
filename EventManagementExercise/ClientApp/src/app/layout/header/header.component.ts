import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {
  public expanded = false;

  constructor() { }

  ngOnInit(): void {
  }

  collapse() {
    this.expanded = false;
  }

  toggle() {
    this.expanded = !this.expanded;
  }
}
