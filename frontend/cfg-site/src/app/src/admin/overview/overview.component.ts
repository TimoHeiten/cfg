import { Component, OnInit } from '@angular/core';
import { ReadConfigService } from '../services/read-config.service';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Observable } from 'rxjs';
import { ConfigValue } from '../model/configValue';

@Component({
  selector: 'app-overview',
  templateUrl: './overview.component.html',
  styleUrls: ['./overview.component.css']
})
export class OverviewComponent implements OnInit {
  keyValueForm: FormGroup;
  public data$: Observable<ConfigValue> = this.readConfig.currentKeySelected$;

  constructor(private readConfig: ReadConfigService, private formBuilder: FormBuilder) {
    this.keyValueForm = this.formBuilder.group({
      key: '',
    });
  }

  ngOnInit(): void {
  }

  onSubmit(): void {
    // Get the value from the "value" input field
    const inputValue = this.keyValueForm.get('key');

    if (!inputValue) {
        return; // todo validate instead
    }
    this.readConfig.getByValue(inputValue.value);
  }
}
