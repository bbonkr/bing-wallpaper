import React from 'react';
import { ImageModel } from '../../models';

interface CardMediaContent {
    title: string;
    subtitle?: string;
}

interface CardMediaContentProps {
    image?: ImageModel;
    content?: CardMediaContent;
}

export const CardMediaContent = ({ image, content }: CardMediaContentProps) => {
    return (
        <div className="media">
            {image && (
                <div className="media-left">
                    <figure>
                        <img src={image.src} alt={image.alt} />
                    </figure>
                </div>
            )}
            {content && (
                <div className="media-content">
                    <p className="title is-4">{content.title}</p>
                    {content.subtitle && (
                        <p className="subtitle is-6">{content.subtitle}</p>
                    )}
                </div>
            )}
        </div>
    );
};
