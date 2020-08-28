import { Directive, TemplateRef } from '@angular/core';

@Directive({
  selector: '[multiLingualEditorTranslation]',
})
export class MultiLingualEditorTranslationDirective {
  constructor(public template: TemplateRef<any>) {}
}
