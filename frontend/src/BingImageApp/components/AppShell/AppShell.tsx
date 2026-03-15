'use client';

import React from 'react';
import { Provider } from 'react-redux';
import { Header, Footer, Container } from '../Layouts';
import { FullSizeImage } from '../FullSizeImage';
import { useStore } from '../../store';
import { LinkModel } from '../../models';

const menuRoutes: LinkModel[] = [
  {
    href: '/',
    title: 'Images',
  },
  {
    href: '/collector',
    title: 'Collector',
  },
  {
    href: '/logs',
    title: 'Logs',
  },
];

interface AppShellProps {
  children: React.ReactNode;
}

export const AppShell = ({ children }: AppShellProps) => {
  const store = useStore();

  const handleClickScrollToTop = () => {
    window.scrollTo({ left: 0, top: 0, behavior: 'smooth' });
  };

  return (
    <Provider store={store}>
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
        {children}
      </Container>
      <Footer onClickScrollToTop={handleClickScrollToTop} />
      <FullSizeImage />
    </Provider>
  );
};

export default AppShell;
