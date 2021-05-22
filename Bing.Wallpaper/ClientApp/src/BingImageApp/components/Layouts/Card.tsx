import React, { PropsWithChildren } from 'react';
import { UiHelper } from '../../lib/UiHelper';
import { Content } from './Content';
import { CardMediaContent } from './CardMediaContent';
import { ImageModel } from '../../models';

interface CardMedia {
    image?: ImageModel;
    title: string;
    subtitle?: string;
}

interface CardProps {
    title?: React.ReactNode;
    cardImage?: ImageModel;
    medias?: CardMedia[];
    classNames?: string[];
}

export const Card = ({
    title,
    cardImage,
    medias,
    classNames,
    children,
}: PropsWithChildren<CardProps>) => {
    return (
        <div
            className={`card ${UiHelper.getClassNames(...(classNames ?? []))}`}
        >
            {cardImage && (
                <div className="card-image">
                    <figure>
                        <img src={cardImage.src} alt={cardImage.alt} />
                    </figure>
                </div>
            )}
            {title && (
                <header className="card-header">
                    <p className="card-header-title">{title}</p>
                </header>
            )}
            <div className="card-content">
                {medias?.map((media) => (
                    <CardMediaContent
                        key={media.title}
                        image={media.image}
                        content={{
                            title: media.title,
                            subtitle: media.subtitle,
                        }}
                    />
                ))}
                <Content>{children}</Content>
            </div>
        </div>
    );
};
