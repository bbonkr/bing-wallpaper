import React from 'react';
import { BrowserRouter, Redirect, Route, Switch } from 'react-router-dom';
import { Provider } from 'react-redux';
import { Empty } from '../Empty';
import { Header, Footer } from '../Layouts';
import { NotFound } from '../NotFount';
import { Container } from '../Layouts';
import { ImagesContent } from '../ImagesContent';
import { FullSizeImage } from '../FullSizeImage';
import { useStore } from '../../store';
import { LinkModel } from '../../models';

import 'bulma/css/bulma.css';
import './style.css';

const menuRoutes: (LinkModel & { Component: React.ReactNode })[] = [
    {
        href: '/',
        title: 'Images',
        Component: <ImagesContent />,
    },
    {
        href: '/logs',
        title: 'Logs',
        Component: <Empty title="Logs" content={<p>Logs</p>} />,
    },
];

export const App = () => {
    const store = useStore();

    const handleClickScrollToTop = () => {
        window.scrollTo({ left: 0, top: 0, behavior: 'smooth' });
    };

    return (
        <Provider store={store}>
            <BrowserRouter>
                <Header menuRoutes={menuRoutes} />
                <Container
                    classNames={[
                        'is-fluid',
                        'pt-6',
                        'pl-0',
                        'pr-0',
                        'm-0',
                        'is-flex',
                        'is-flex-direction-column',
                    ]}
                >
                    <Switch>
                        {menuRoutes.map((route) => (
                            <Route path={route.href} exact key={route.href}>
                                {route.Component}
                            </Route>
                        ))}

                        <Route path="/404">
                            <NotFound />
                        </Route>
                        <Redirect to="/404" />
                    </Switch>
                </Container>
                <Footer onClickScrollToTop={handleClickScrollToTop} />
                <FullSizeImage />
            </BrowserRouter>
        </Provider>
    );
};
