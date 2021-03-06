import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Make } from 'src/app/_models/make';
import { MakeService } from 'src/app/_services/make.service';

@Component({
  selector: 'app-make-edit',
  templateUrl: './make-edit.component.html',
  styleUrls: ['./make-edit.component.css']
})
export class MakeEditComponent implements OnInit {
  make: Make;

  constructor(private makeService: MakeService, private route: ActivatedRoute, private router: Router) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      // tslint:disable-next-line: no-string-literal
      this.make = data['make'];
    });
  }

  updateMake() {
    this.makeService.updateMake(this.make.id, this.make).subscribe();
    this.router.navigate(['makes']);
  }

}
