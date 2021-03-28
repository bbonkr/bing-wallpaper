import React from 'react';
import { BrowserRouter, Redirect, Route, Switch } from 'react-router-dom';
import { Provider } from 'react-redux';
import { Empty } from '../Empty';
import { Header } from '../Header';
import { Footer } from '../Footer';
import { NotFound } from '../NotFount';
import { PageWrapper, ContentWrapper } from '../Layouts';
import { ImagesContent } from '../ImagesContent';
import { FullSizeImage } from '../FullSizeImage';
import { useStore } from '../../store';
import 'halfmoon/css/halfmoon.css';

export const App = () => {
    const store = useStore();
    return (
        <Provider store={store}>
            <BrowserRouter>
                <PageWrapper withNavbar withNavbarFixedBottom>
                    <Header />

                    <ContentWrapper>
                        <Switch>
                            <Route path="/" exact>
                                <ImagesContent />
                            </Route>
                            <Route path="/logs" exact>
                                <Empty title="Logs" content={<p>Logs</p>} />
                            </Route>
                            <Route path="/404">
                                <NotFound />
                            </Route>
                            <Redirect to="/404" />
                        </Switch>
                    </ContentWrapper>

                    <Footer />
                </PageWrapper>
                <FullSizeImage />
            </BrowserRouter>
        </Provider>
    );
};
