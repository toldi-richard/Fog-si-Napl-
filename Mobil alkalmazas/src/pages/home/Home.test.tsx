import Home from "./home.page";
import { render, screen } from '@testing-library/react';

test('HomePage should have a title of ,,Fogási Napló,,', async ()=>
{
    render(<Home />);
    const cardTitleText: HTMLElement = screen.getByText("Fogási Napló");
    expect(cardTitleText).toBeInTheDocument;
});

test('HomePage should have a icon with a title ,,Add new catch,,', async ()=>
{
    const {findByTitle} = render(<Home />);
    await findByTitle("Add new catch")
});

test('Has AddNewCatchButton' , async()=> 
{
    const {findAllByTitle} = render(<Home />);
    await findAllByTitle('Add new catch');
});

test('HomePage should have an item with email ,,Agoston1@gmail.com,,', async ()=>
{
    const email: string = 'Agoston1@gmail.com';

    const {findByText} = render(<Home />);
    await findByText(email)
});

test('HomePage should have an item with email in array', async ()=>
{
    const emails: string[] = ['Agoston1@gmail.com', 'user12@gmail.com','Klarika22@gmail.com'];

    const {findByText} = render(<Home />);
    await findByText(emails[0])
    await findByText(emails[1])
    await findByText(emails[2])
});