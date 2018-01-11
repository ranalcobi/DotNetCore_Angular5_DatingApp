import { Component, OnInit } from '@angular/core';
import { Http } from '@angular/http';


@Component({
  selector: 'app-value',
  templateUrl: './value.component.html',
  styleUrls: ['./value.component.css']
})
export class ValueComponent implements OnInit {
  values: any;

  constructor(private _http: Http) { }

  ngOnInit() {
    this.getValues();
  }

  getValues() {
    this._http.get('http://localhost:5000/api/Values').subscribe(res => {
      this.values = res.json();
      console.log(this.values);
    });
  }

}
