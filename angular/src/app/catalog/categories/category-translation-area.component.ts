import { Component, Injector, Input } from '@angular/core';
import { CategoryTranslationEditDto } from '@shared/service-proxies/service-proxies';
import * as _ from 'lodash';
import { AppComponentBase } from '@shared/app-component-base';
import { ControlContainer, NgForm } from '@angular/forms';

@Component({
  selector: 'category-translation-area',
  templateUrl: './category-translation-area.component.html',
  viewProviders: [{ provide: ControlContainer, useExisting: NgForm }],
})
export class CategoryTranslationAreaComponent extends AppComponentBase {
  languages: abp.localization.ILanguageInfo[] = [];
  private _translations: CategoryTranslationEditDto[] = [];

  get translations(): CategoryTranslationEditDto[] {
    return this._translations;
  }
  @Input() set translations(val: CategoryTranslationEditDto[]) {
    this.bindLanguages();
    this.bindInitialTranslations();
    if (val) this.bindExistingTranslations(val);
  }

  constructor(injector: Injector) {
    super(injector);
  }

  private bindLanguages(): void {
    this.languages = _.filter(abp.localization.languages, (l) => !l.isDisabled);
    const currentLanguage = abp.localization.currentLanguage;

    _.remove(this.languages, (x) => x.name === currentLanguage.name);
    this.languages.unshift(currentLanguage);
  }

  private bindInitialTranslations(): void {
    this._translations = [];
    const defaultTranslation = new CategoryTranslationEditDto();
    defaultTranslation.isDefault = true;
    this._translations.push(defaultTranslation);

    _.forEach(this.languages, (language, i) => {
      const translation = new CategoryTranslationEditDto();
      translation.language = language.name;

      this._translations.push(translation);
    });
    debugger;
  }

  private bindExistingTranslations(
    existingTranslations: CategoryTranslationEditDto[]
  ) {
    _.forEach(this._translations, (initialTranslation, i: number) => {
      debugger;
      var existingTranslation = _.find(
        existingTranslations,
        (x) => x.language === initialTranslation.language
      );

      if (existingTranslation) {
        this._translations[i] = existingTranslation;
      } else {
        existingTranslations.push(initialTranslation);
      }

      // const pushedTranslation = existingTranslation || initialTranslation;
      // pushedTranslation.isDefault =
      //   pushedTranslation.language === abp.localization.currentLanguage.name;
    });
  }
}
