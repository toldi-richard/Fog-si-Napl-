export interface HttpResponse<T> extends Response
{
    responseBody?: T;
}

export const HTTP = async<T> (request: RequestInfo): Promise<T | undefined> =>
{
    const response: HttpResponse<T> = await fetch(request);

    response.responseBody = await response.json();

    return response.responseBody;
}





