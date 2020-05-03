import { CommonModule } from '@angular/common';
import { NgModule, ModuleWithProviders } from '@angular/core';
import { AbpModule } from '@abp/abp.module';
import { RouterModule } from '@angular/router';
import { NgxPaginationModule } from 'ngx-pagination';
import { TabsModule } from 'ngx-bootstrap/tabs';

import { AppSessionService } from './session/app-session.service';
import { AppUrlService } from './nav/app-url.service';
import { AppAuthService } from './auth/app-auth.service';
import { AppRouteGuard } from './auth/auth-route-guard';
import { LocalizePipe } from '@shared/pipes/localize.pipe';

import { AbpPaginationControlsComponent } from './components/pagination/abp-pagination-controls.component';
import { AbpValidationSummaryComponent } from './components/validation/abp-validation.summary.component';
import { AbpModalHeaderComponent } from './components/modal/abp-modal-header.component';
import { AbpModalFooterComponent } from './components/modal/abp-modal-footer.component';
import { LayoutStoreService } from './layout/layout-store.service';

import { BusyDirective } from './directives/busy.directive';
import { EqualValidator } from './directives/equal-validator.directive';
import { MultiLingualEditorComponent, MultiLingualEditorTranslationsDirective, MultiLingualEditorDefaultDirective } from './components/translation-editor/multi-lingual-editor.component';
import { MultiLingualModelService } from './components/translation-editor/multi-lingual-model.service';

@NgModule({
  imports: [
    CommonModule,
    AbpModule,
    RouterModule,
    NgxPaginationModule,
    TabsModule.forRoot(),
  ],
  declarations: [
    AbpPaginationControlsComponent,
    AbpValidationSummaryComponent,
    AbpModalHeaderComponent,
    AbpModalFooterComponent,
    LocalizePipe,
    BusyDirective,
    EqualValidator,
    MultiLingualEditorComponent,
    MultiLingualEditorTranslationsDirective,
    MultiLingualEditorDefaultDirective,
  ],
  exports: [
    AbpPaginationControlsComponent,
    AbpValidationSummaryComponent,
    AbpModalHeaderComponent,
    AbpModalFooterComponent,
    LocalizePipe,
    BusyDirective,
    EqualValidator,
    MultiLingualEditorComponent,
    MultiLingualEditorTranslationsDirective,
    MultiLingualEditorDefaultDirective,
  ],
})
export class SharedModule {
  static forRoot(): ModuleWithProviders {
    return {
      ngModule: SharedModule,
      providers: [
        AppSessionService,
        AppUrlService,
        AppAuthService,
        AppRouteGuard,
        LayoutStoreService,
        MultiLingualModelService,
      ],
    };
  }
}
