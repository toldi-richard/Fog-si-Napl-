import React from 'react';

class ErrorBoundary extends React.Component<any, { hasError: boolean }> {
    constructor(props: any) {
        super(props);
        this.state = { hasError: false };
    }

    static getDerivedStateFromError(error: any) {
        // Update state so the next render will show the fallback UI.
        return { hasError: true };
    }

    componentDidCatch(error: any, errorInfo: any) {
        console.log("ERROR!");
        this.setState({ hasError: true });
        // You can also log the error to an error reporting service
        //   logErrorToMyService(error, errorInfo);
    }

    render() {
        console.log(this.state);
        if (this.state.hasError) {
            // You can render any custom fallback UI
            return <h1>Caught The Error</h1>;
        }

        return this.props.children;
    }
}

export default ErrorBoundary;