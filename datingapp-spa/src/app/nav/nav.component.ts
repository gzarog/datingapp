import { Component, OnInit } from '@angular/core';

@Component({
    selector: 'app-nav',
    templateUrl: './nav.component.html',
    styleUrls: ['./nav.component.css']
})
/** nav component*/
export class NavComponent implements OnInit {
  model: any = {};
/** nav ctor */
    constructor() {
     
  }

  ngOnInit() { }

  login() {
    console.log(this.model);
  }
}
