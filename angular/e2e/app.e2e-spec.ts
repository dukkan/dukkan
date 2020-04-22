import { DukkanTemplatePage } from './app.po';

describe('Dukkan App', function() {
  let page: DukkanTemplatePage;

  beforeEach(() => {
    page = new DukkanTemplatePage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
