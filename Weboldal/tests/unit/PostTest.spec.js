import { mount } from '@vue/test-utils'
import Post from '@/views/Post.vue'

describe('Post tesztelése',  () => {
    test('Horgászhely mező kitöltésének tesztje', async () => {
        const wrapper = mount(Post)
        const input = wrapper.find('input[name="fishpondNumber"]')
        await input.setValue('1')
        expect(wrapper.vm.fishpondNumber).toBe('1')
    })
    test('Azonosító mező kitöltésének tesztje', async () => {
        const wrapper = mount(Post)
        const input = wrapper.find('input[name="identifierNumber"]')
        await input.setValue('123987')
        expect(wrapper.vm.identifierNumber).toBe('123987')
    })
    test('Helyszín mező kitöltésének tesztje', async () => {
        const wrapper = mount(Post)
        const select = wrapper.find('select[name="fishpond"]')
        await select.setValue('2')
        expect(wrapper.vm.fishpond).toBe('2')
    })
    test('Halfaj mező kitöltésének tesztje', async () => {
        const wrapper = mount(Post)
        const select = wrapper.find('select[name="fish"]')
        await select.setValue('amur')
        expect(wrapper.vm.fish).toBe('amur')
    })
    test('Halsúly mező kitöltésének tesztje', async () => {
        const wrapper = mount(Post)
        const select = wrapper.find('select[name="weightOfFish"]')
        await select.setValue('4')
        expect(wrapper.vm.weightOfFish).toBe('4')
    })
})