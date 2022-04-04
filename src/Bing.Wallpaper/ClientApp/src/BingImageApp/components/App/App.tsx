import React from 'react';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import { Redirect } from '../Redirect';
import { Provider } from 'react-redux';
import { Header, Footer } from '../Layouts';
import { Container } from '../Layouts';
import { FullSizeImage } from '../FullSizeImage';
import { useStore } from '../../store';
import { LinkModel } from '../../models';
import { Loading } from '../Loading';

import 'bulma/css/bulma.css';
import './style.css';

const NotFound = React.lazy(() => import('../NotFount'));
const ImagesContent = React.lazy(() => import('../ImagesContent'));
const LogsContent = React.lazy(() => import('../LogsContent'));
const ImageCollector = React.lazy(() => import('../ImageCollector'));

const menuRoutes: (LinkModel & { Component: React.ReactNode })[] = [
    {
        href: '/',
        title: 'Images',
        Component: <ImagesContent />,
    },
    {
        href: '/collector',
        title: 'Collector',
        Component: <ImageCollector />,
    },
    {
        href: '/logs',
        title: 'Logs',
        Component: <LogsContent />,
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
                <React.Suspense fallback={<Loading />}>
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
                        <Routes>
                            {menuRoutes.map((route) => (
                                <Route
                                    path={route.href}
                                    key={route.href}
                                    element={route.Component}
                                />
                            ))}

                            <Route path="/404">
                                <NotFound />
                            </Route>
                            <Redirect to="/404" />
                        </Routes>
                    </Container>
                    <Footer onClickScrollToTop={handleClickScrollToTop} />
                    <FullSizeImage />
                </React.Suspense>
            </BrowserRouter>
        </Provider>
    );
};
