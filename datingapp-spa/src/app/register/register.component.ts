import { Component, OnInit, Input, EventEmitter, Output  } from '@angular/core';
import { AuthService } from '../_services/auth.service';

@Component({
    selector: 'app-register',
    templateUrl: './register.component.html',
    styleUrls: ['./register.component.css']
})
/** register component*/
export class RegisterComponent implements OnInit {
  @Output() cancelRegister= new EventEmitter();
  model: any = {};

/** register ctor */
    constructor(private authService:AuthService) {

  }
  ngOnInit() {

  }

  register() {
    this.authService.register(this.model).subscribe(() => {
      console.log('register success');
    }, error => {
      console.log(error);
    });
  }

  cancel() {
    this.cancelRegister.emit(false);
    console.log('cancel');
  }
}
