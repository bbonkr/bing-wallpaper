import React from 'react';
import { BrowserRouter, Redirect, Route, Switch, Link } from 'react-router-dom';
import { Empty } from '../Empty';
import { Header } from '../Header';
import { Footer } from '../Footer';
import { NotFound } from '../NotFount';
import { PageWrapper, ContentWrapper } from '../Layouts';

import 'halfmoon/css/halfmoon.css';

export const App = () => {
    return (
        <BrowserRouter>
            <PageWrapper withNavbar withNavbarFixedBottom>
                <Header />

                <ContentWrapper>
                    <Switch>
                        <Route path="/" exact>
                            <Empty title="Images" content={<p>Images</p>} />
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
        </BrowserRouter>
    );
};
