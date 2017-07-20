import { MySpotifyBillboardPage } from './app.po';

describe('my-spotify-billboard App', () => {
  let page: MySpotifyBillboardPage;

  beforeEach(() => {
    page = new MySpotifyBillboardPage();
  });

  it('should display welcome message', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('Welcome to app!');
  });
});
