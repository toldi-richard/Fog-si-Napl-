import { mount } from '@vue/test-utils'
import Signup from '@/views/Signup.vue'

describe('Signup tesztelése',  () => {
    test('Email mező kitöltésének tesztje', async () => {
        const wrapper = mount(Signup)
        const input = wrapper.find('input[type="email"]') 
        await input.setValue('valaki@gmail.com')
        expect(wrapper.vm.register_form.email).toBe('valaki@gmail.com')
    })
    test('Jelszó mező kitöltésének tesztje', async () => {
        const wrapper = mount(Signup)
        const input = wrapper.find('input[type="password"]') 
        await input.setValue('12345')
        expect(wrapper.vm.register_form.password).toBe('12345')
    })
})