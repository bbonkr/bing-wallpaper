import type { Metadata } from 'next';
import './globals.css';

export const metadata: Metadata = {
  title: 'Bing Wallpaper',
  description: 'Bing wallpaper collector and viewer',
};

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <html lang="en">
      <body>{children}</body>
    </html>
  );
}
